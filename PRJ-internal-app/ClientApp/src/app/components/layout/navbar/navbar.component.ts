import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { faUserCircle } from '@fortawesome/free-solid-svg-icons';
import { faBars } from '@fortawesome/free-solid-svg-icons';
import { Observable } from 'rxjs';
import { AccountService } from '../../../../services/Account/account.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css'],
})
export class NavbarComponent implements OnInit {
  faUserCircle = faUserCircle;
  faBars = faBars;
  isLoggedIn$: Observable<boolean>;
  constructor(private router: Router, private accountService: AccountService) { }

  ngOnInit(): void {
    this.isLoggedIn$ = this.accountService.isLoggedIn;
  }

  logout() {
    this.accountService.logout();
  }

  navigateToMainMenu() {
    this.router.navigate(['']);
  }
}
