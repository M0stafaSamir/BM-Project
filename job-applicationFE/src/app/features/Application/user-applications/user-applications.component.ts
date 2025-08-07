import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { RouterModule } from '@angular/router';
import { ApplicationService } from '../../../core/services/application.service';
import { GetApplication } from '../../../shared/models/application.model';
import { ApplicationCardComponent } from '../../../shared/components/application-card/application-card.component';
import { LoaderComponent } from '../../../shared/components/loader/loader.component';

@Component({
  selector: 'app-user-applications',
  standalone: true,
  imports: [
    CommonModule,
    RouterModule,
    ApplicationCardComponent,
    LoaderComponent,
  ],
  templateUrl: './user-applications.component.html',
  styleUrl: './user-applications.component.css',
})
export class UserApplicationsComponent implements OnInit {
  isLoading: boolean = false;
  constructor(private appService: ApplicationService) {}

  userApplications: GetApplication[] = [];
  ngOnInit(): void {
    this.getUserApplications();
  }

  getUserApplications() {
    this.isLoading = true;
    this.appService.getUserApplications().subscribe({
      next: (res: any) => {
        console.log('User Applications:', res.data);
        this.userApplications = res.data;
        this.isLoading = false;
      },
      error: (err) => {
        console.error('Error fetching user applications', err);
        this.isLoading = false;
      },
    });
  }

  deleteApplication(applicationId: number) {
    this.appService.deleteApplication(applicationId).subscribe({
      next: (res: any) => {
        console.log(res, 'Application deleted successfully');
        this.getUserApplications();
      },
      error: (err) => {
        console.error('Error deleting application', err);
      },
    });
  }
}
