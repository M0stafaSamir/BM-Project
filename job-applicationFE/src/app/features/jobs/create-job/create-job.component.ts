import { createJob } from '../../../shared/models/job.model';
import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { JobService } from '../../../core/services/job.service';
import { AuthService } from '../../../core/services/auth.service';

@Component({
  selector: 'app-create-job',
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: './create-job.component.html',
  styleUrl: './create-job.component.css',
})
export class CreateJobComponent implements OnInit {
  constructor(
    private jobService: JobService,
    private authService: AuthService,
    private fb: FormBuilder
  ) {}

  jobForm!: FormGroup;
  errorMessage = '';

  ngOnInit(): void {
    this.jobForm = this.fb.group({
      title: ['', Validators.required],
      description: [''],
      company: ['', Validators.required],
      salary: [''],
      email: ['', [Validators.required, Validators.email]],
      location: [''],
      postedAt: [null],
      createdById: [null],
    });
  }

  onSubmit() {
    if (this.jobForm.invalid) {
      this.jobForm.markAllAsTouched();
      return;
    }
    this.errorMessage = '';

    this.jobForm.patchValue({
      postedAt: new Date(),
      createdById: this.authService.getUserId(),
    });
    console.log('form', this.jobForm.value);

    this.jobService.createJob(this.jobForm.value).subscribe({
      next: (res: any) => {
        this.jobForm.reset();
        alert(res.message);
      },
      error: (err) => {
        console.error('Error creating job', err);
        this.errorMessage = err.error?.message || 'Failed to create job';
      },
    });
  }
}
