import { Component } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { AuthService } from '../../../core/services/auth.service';
import { Router, RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { Register } from '../../../shared/models/user.model';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css',
})
export class RegisterComponent {
  isLoading = false;
  errorMessage = '';
  successMessage = '';
  registerForm = this.fb.group({
    fname: ['', Validators.required],
    lname: ['', Validators.required],
    email: ['', [Validators.required, Validators.email]],
    phoneNo: ['', Validators.required],
    address: ['', Validators.required],
    password: ['', [Validators.required, Validators.minLength(6)]],
    confirmPassword: ['', Validators.required],
  });

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router
  ) {}

  onSubmit() {
    if (this.registerForm.invalid) {
      this.errorMessage = 'Please fill all required fields correctly.';
      this.registerForm.markAllAsTouched();
      return;
    }

    const { fname, lname, email, password, confirmPassword, phoneNo, address } =
      this.registerForm.value;

    if (password !== confirmPassword) {
      this.errorMessage = 'Passwords do not match';
      return;
    }

    const registerData: Register = {
      fname: fname!,
      lname: lname!,
      email: email!,
      password: password!,
      confirmPassword: confirmPassword!, // if backend really needs it
      address: address!,
      phoneNo: phoneNo!,
    };

    this.isLoading = true;
    this.errorMessage = '';

    this.authService.Register(registerData).subscribe({
      next: (res: any) => {
        console.log('Registration success:', res);
        this.isLoading = false;
        this.successMessage = res.message;
        setTimeout(() => {
          this.router.navigate(['/login']);
        }, 3000);
      },
      error: (err) => {
        console.error('Registration error:', err);
        this.errorMessage = err.error?.message || 'Registration failed';
        this.isLoading = false;
      },
    });
  }
}
