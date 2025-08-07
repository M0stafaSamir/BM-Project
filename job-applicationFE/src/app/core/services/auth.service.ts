import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from './environment';
import { Login, Register } from '../../shared/models/user.model';
import { jwtDecode } from 'jwt-decode';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  constructor(private http: HttpClient, private router: Router) {}

  private apiUrl = `${environment.apiUrl}/auth`;

  login(loginData: Login) {
    return this.http.post(`${this.apiUrl}/login`, loginData);
  }
  Register(registerData: Register) {
    return this.http.post(`${this.apiUrl}/register`, registerData);
  }
  saveToken(token: string) {
    localStorage.setItem('token', token);
  }
  logout() {
    localStorage.removeItem('token');
    this.router.navigate(['/login']);
  }

  getToken(): string | null {
    const token = localStorage.getItem('token');
    if (!token) {
      console.log('No token found in localStorage');
    }
    return token;
  }

  getDecodedToken(): any {
    const token = this.getToken();
    if (token) {
      try {
        return jwtDecode(token);
      } catch (e) {
        console.error('Invalid token', e);
      }
    }
    return null;
  }

  getUserRole(): string | null {
    const decoded = this.getDecodedToken();
    return decoded
      ? decoded['http://schemas.microsoft.com/ws/2008/06/identity/claims/role']
      : 'User';
  }

  isAuthenticated(): boolean {
    const token = this.getToken();
    if (!token) {
      return false;
    }
    return true;
  }
  getUserId(): string | null {
    const decoded = this.getDecodedToken();
    return decoded
      ? decoded[
          'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier'
        ]
      : null;
  }
}
