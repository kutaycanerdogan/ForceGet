// header/header.component.ts
import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss'],
})
export class HeaderComponent implements OnInit {
  isLoggedIn = false;
  userEmail = '';

  constructor(private authService: AuthService, private router: Router) {}

  ngOnInit(): void {
    this.checkAuthStatus();
  }

  checkAuthStatus(): void {
    const token = this.authService.getToken();
    this.isLoggedIn = !!token;

    if (this.isLoggedIn) {
      // You can decode the JWT token to get user email
      // For now, we'll just show a placeholder
      this.userEmail = 'User';
    }
  }

  logout(): void {
    this.authService.logout();
    this.isLoggedIn = false;
    this.userEmail = '';
    this.router.navigate(['/login']);
  }
}
