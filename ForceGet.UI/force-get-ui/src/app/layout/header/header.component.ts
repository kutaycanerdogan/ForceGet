import { Component } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss'],
})
export class HeaderComponent {
  isLoggedIn = false;
  userEmail = '';

  constructor(private auth: AuthService, private router: Router) {
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
    this.router.navigate(['/login']);
  }
}
