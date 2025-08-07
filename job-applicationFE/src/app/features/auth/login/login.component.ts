import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { AuthService } from '../../../core/services/auth.service';
import { Router, RouterModule } from '@angular/router';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterModule, FormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css',
})
export class LoginComponent {
  loginForm: FormGroup;
  loading = false;
  errorMessage = '';

  showPassword = false;

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router
  ) {
    this.loginForm = this.loginForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required],
    });
  }

  togglePassword(event: Event) {
    this.showPassword = (event.target as HTMLInputElement).checked;
  }

  onSubmit() {
    if (this.loginForm.invalid) {
      this.loginForm.markAllAsTouched();
      return;
    }

    this.loading = true;
    this.errorMessage = '';

    this.authService.login(this.loginForm.value).subscribe({
      next: (res: any) => {
        console.log('Login successful', res);
        if (res.data) {
          this.authService.saveToken(res.data);
          const userRole = this.authService.getUserRole();
          console.log('User role:', userRole);
          if (userRole === 'Admin') {
            this.router.navigate(['/']);
          } else {
            this.router.navigate(['/']);
          }
        }
        this.loading = false;
      },
      error: (err) => {
        console.error('Login error', err);
        this.errorMessage = err.error?.message || 'Invalid email or password';
        this.loading = false;
      },
    });
  }
}
