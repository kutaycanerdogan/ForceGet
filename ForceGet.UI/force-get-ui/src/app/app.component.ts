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

  constructor(private auth: AuthService, private overlay: OverlayContainer) {}

  ngOnInit(): void {
    this.auth.isLoggedIn$.subscribe((status) => {
      this.isLoggedIn = status;
    });

    this.auth.decoded$.subscribe((decoded) => {
      this.userEmail =
        decoded?.email ||
        decoded?.[
          'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress'
        ] ||
        '';
    });
  }

  logout(): void {
    this.auth.logout();
  }
}
