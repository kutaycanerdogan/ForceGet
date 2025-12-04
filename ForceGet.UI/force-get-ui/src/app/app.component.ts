// app.component.ts
import { Component, OnInit } from '@angular/core';
import { AuthService } from './services/auth.service';
import { OverlayContainer } from '@angular/cdk/overlay';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent implements OnInit {
  isLoggedIn = false;
  userEmail = '';

  constructor(
    private authService: AuthService,
    private overlay: OverlayContainer
  ) { }
  
  ngAfterViewInit(): void {
    const container = this.overlay.getContainerElement();
    if (container.parentElement !== document.body) {
      document.body.appendChild(container);
    }
  }

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
  }
}
