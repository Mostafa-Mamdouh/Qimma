import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { faUserCircle } from '@fortawesome/free-solid-svg-icons';
import { faBars } from '@fortawesome/free-solid-svg-icons';
import {faLink, faGlobe, faPlus, faXmark ,faCircleUser,faHouse, faBrain,faUsersLine,faPersonBurst,faEnvelope} from '@fortawesome/free-solid-svg-icons';
import { Observable } from 'rxjs';
import { AccountService } from 'src/app/services/Account/account.service';
import { SharedService } from 'src/app/services/Shared/shared.service';

@Component({
  selector: 'app-footer',
  templateUrl: './Footer.component.html',
  styleUrls: ['./Footer.component.css'],
})
export class FooterComponent implements OnInit {
  faUserCircle = faUserCircle;
  faBars = faBars;
  faPlus = faPlus;
  faXmark = faXmark;
  faCircleUser =faCircleUser;
  faHouse=faHouse;
  faBrain=faBrain;
  faPersonBurst=faPersonBurst;
  faUsersLine=faUsersLine;
  faEnvelope=faEnvelope;
  faGlobe=faGlobe;
  faLink=faLink;
  isLoggedIn$: Observable<boolean>;
  constructor(private router: Router,
    private sharedService: SharedService,
    private accountService: AccountService
      )    {}

  ngOnInit(): void {
    this.isLoggedIn$ = this.accountService.isLoggedIn;
  }

  logout() {
    this.accountService.logout();
  }

  navigateToMainMenu() {
    this.router.navigate(['/main']);
  }
}
