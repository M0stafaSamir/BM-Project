import { JobService } from './../../../core/services/job.service';
import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { GetJob } from '../../../shared/models/job.model';

import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { ApplicationService } from '../../../core/services/application.service';
import { AuthService } from '../../../core/services/auth.service';
import { LoaderComponent } from '../../../shared/components/loader/loader.component';

@Component({
  selector: 'app-job-details',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, LoaderComponent],
  templateUrl: './job-details.component.html',
  styleUrl: './job-details.component.css',
})
export class JobDetailsComponent implements OnInit {
  applyForm!: FormGroup;
  jobId!: number;
  applicationForm: boolean = false;
  errorMessage: string = '';
  resumeFile: File | null = null;
  job!: GetJob;
  isLoading: boolean = false;

  constructor(
    private route: ActivatedRoute,
    private jobService: JobService,
    private authService: AuthService,
    private applicationService: ApplicationService,
    private fb: FormBuilder,
    private router: Router
  ) {}

  ngOnInit() {
    this.applyForm = this.fb.group({
      coverLetter: ['', [Validators.required, Validators.minLength(20)]],
    });

    // Get job ID from route
    this.jobId = Number(this.route.snapshot.paramMap.get('id'));
    if (this.jobId) {
      this.getJob(this.jobId);
    } else {
      console.error('No job ID provided in route');
    }
  }

  getJob(id: number) {
    this.isLoading = true;
    this.jobService.getJobById(id).subscribe({
      next: (res: any) => {
        this.job = res.data;
        this.isLoading = false;
        console.log('Job:', this.job);
      },
      error: (err) => {
        console.error('Error fetching job details', err);
        this.isLoading = false;
        this.errorMessage = err.error?.message || 'Failed to load job details';
      },
    });
  }

  //open form if authenticated
  openApplicationForm() {
    if (this.authService.isAuthenticated()) {
      this.applicationForm = true;
    } else {
      this.router.navigate(['/login']);
    }
  }

  // Handle file selection
  onFileSelected(event: any) {
    const file = event.target.files[0];
    if (file) {
      this.resumeFile = file;
    }
  }

  // Submit application
  onSubmit() {
    if (!this.resumeFile) {
      alert('Please select a resume file.');
      return;
    }

    if (this.applyForm.invalid) {
      this.applyForm.markAllAsTouched();
      alert('Please fill all required fields correctly.');
      return;
    }

    const formData = new FormData();
    formData.append('jobId', this.jobId.toString());
    formData.append('coverLetter', this.applyForm.value.coverLetter);
    formData.append('appliedAt', new Date().toISOString());
    formData.append('resumeFile', this.resumeFile);

    this.applicationService.applyForJob(formData).subscribe({
      next: (res: any) => {
        console.log('Application submitted successfully:', res);
        this.getJob(this.jobId);
        alert('Application submitted successfully!');
        this.applyForm.reset();
        this.resumeFile = null;
        this.applicationForm = false;
      },
      error: (err) => {
        console.error('Error submitting application:', err);
        this.errorMessage =
          err.error?.message || 'Application submission failed';
      },
    });
  }
}
