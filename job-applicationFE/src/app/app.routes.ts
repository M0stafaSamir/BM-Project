import { Routes } from '@angular/router';
import { LoginComponent } from './features/auth/login/login.component';
import { RegisterComponent } from './features/auth/register/register.component';
import { AdminDashboardComponent } from './features/dashborards/admin-dashboard/admin-dashboard.component';
import { UserDashboardComponent } from './features/dashborards/user-dashboard/user-dashboard.component';
import { UnauthorizedComponent } from './shared/components/unauthorized/unauthorized.component';
import { userGuard } from './core/guards/user.guard';
import { adminGuard } from './core/guards/admin.guard';
import { authGuard } from './core/guards/auth.guard';
import { NotfoundComponent } from './shared/components/notfound/notfound.component';
import { HomeComponent } from './features/home/home.component';
import { JobListComponent } from './features/jobs/job-list/job-list.component';
import { JobDetailsComponent } from './features/jobs/job-details/job-details.component';
import { UserApplicationsComponent } from './features/Application/user-applications/user-applications.component';
import { CreateJobComponent } from './features/jobs/create-job/create-job.component';
import { JobApplicationsComponent } from './features/Application/job-applications/job-applications.component';

export const routes: Routes = [
  //auth routes
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: '', component: HomeComponent },
  //job routes
  { path: 'jobs', component: JobListComponent },
  { path: 'job-details/:id', component: JobDetailsComponent },
  {
    path: 'create-job',
    component: CreateJobComponent,
    canActivate: [authGuard, adminGuard],
  },

  //application routes
  {
    path: 'my-applications',
    component: UserApplicationsComponent,
    canActivate: [authGuard, userGuard],
  },
  {
    path: 'job-applications/:id',
    component: JobApplicationsComponent,
    canActivate: [authGuard, authGuard],
  },

  //dashboard routes
  {
    path: 'admin-dashboard',
    component: AdminDashboardComponent,
    canActivate: [authGuard, adminGuard],
  },
  {
    path: 'dashboard',
    component: UserDashboardComponent,
    canActivate: [authGuard, userGuard],
  },
  { path: 'unauthorized', component: UnauthorizedComponent },

  { path: '**', component: NotfoundComponent },
];
