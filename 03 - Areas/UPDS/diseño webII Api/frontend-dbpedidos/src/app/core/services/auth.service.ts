import { Injectable } from '@angular/core';
import axios from 'axios';
import { environment } from '../../../environments/environment';
import { Router } from '@angular/router';

export interface UserLoginDto {
  userName: string;
  password: string;
}

export interface UserRegisterDto {
  userName: string;
  password: string;
}

@Injectable({ providedIn: 'root' })
export class AuthService {
  private readonly apiUrl = `${environment.apiUrl}/auth`;

  constructor(private router: Router) {}

  async login(dto: UserLoginDto): Promise<void> {
    const res = await axios.post(`${this.apiUrl}/login`, dto);
    const token = res.data.token;
    localStorage.setItem('token', token);
    localStorage.setItem('userName', dto.userName);
    console.log('Login successful - saved username:', dto.userName);
  }

  async register(dto: UserRegisterDto): Promise<void> {
    const res = await axios.post(`${this.apiUrl}/register`, dto);
    const token = res.data.token;
    localStorage.setItem('token', token);
    localStorage.setItem('userName', dto.userName);
  }

  logout() {
    localStorage.removeItem('token');
    localStorage.removeItem('userName');
    this.router.navigate(['/login']); // 👈 redirige después del logout
  }

  getToken(): string | null {
    return localStorage.getItem('token');
  }

  getUserName(): string | null {
    const userName = localStorage.getItem('userName');
    console.log('AuthService.getUserName() called, returning:', userName);
    return userName;
  }

  isAuthenticated(): boolean {
    return !!this.getToken();
  }
}
