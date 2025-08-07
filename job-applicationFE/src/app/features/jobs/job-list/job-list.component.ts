import { JobService } from './../../../core/services/job.service';
import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { GetJob } from '../../../shared/models/job.model';
import { JobCardComponent } from '../../../shared/components/job-card/job-card.component';
import { LoaderComponent } from '../../../shared/components/loader/loader.component';

@Component({
  selector: 'app-job-list',
  standalone: true,
  imports: [CommonModule, JobCardComponent, LoaderComponent],
  templateUrl: './job-list.component.html',
  styleUrl: './job-list.component.css',
})
export class JobListComponent implements OnInit {
  constructor(private JobService: JobService) {}

  jobs: GetJob[] = [];
  isLoading = false;

  ngOnInit() {
    this.isLoading = true;
    this.JobService.getAllJobs().subscribe({
      next: (res: any) => {
        this.jobs = res.data;
        this.isLoading = false;
        console.log('Jobs:', this.jobs);
      },
      error: (err) => {
        console.error('Error fetching jobs', err);
        this.isLoading = false;
      },
    });
  }
}
