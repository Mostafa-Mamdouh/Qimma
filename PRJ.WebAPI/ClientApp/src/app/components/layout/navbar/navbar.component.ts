import { DOCUMENT } from '@angular/common';
import { Component, Inject, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { faUserCircle } from '@fortawesome/free-solid-svg-icons';
import { faBars } from '@fortawesome/free-solid-svg-icons';
import {
  faRedo,
  faGlobe,
  faPlus,
  faXmark,
  faCircleUser,
  faHouse,
  faBrain,
  faUsersLine,
  faPersonBurst,
  faEnvelope,
} from '@fortawesome/free-solid-svg-icons';
import { TranslateService } from '@ngx-translate/core';
import { CookieService } from 'ngx-cookie-service';
import { Observable } from 'rxjs';
import { AccountService } from 'src/app/services/Account/account.service';
import { SharedService } from 'src/app/services/Shared/shared.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css'],
})
export class NavbarComponent implements OnInit {
  isLoggedIn$: Observable<boolean>;

  faUserCircle = faUserCircle;
  faBars = faBars;
  faPlus = faPlus;
  faXmark = faXmark;
  faCircleUser = faCircleUser;
  faHouse = faHouse;
  faBrain = faRedo;
  faPersonBurst = faPersonBurst;
  faUsersLine = faUsersLine;
  faEnvelope = faEnvelope;
  faGlobe = faGlobe;
  public lang: string = 'en';
  htmlTag = this.document.getElementsByTagName('html')[0] as HTMLHtmlElement;

  constructor(
    private router: Router,
    private sharedService: SharedService,
    private cookieService: CookieService,
    @Inject(DOCUMENT) private document: Document,
    public translate: TranslateService,
    private accountService: AccountService
  ) {}

  ngOnInit(): void {
    this.isLoggedIn$ = this.accountService.isLoggedIn;
    this.lang = this.sharedService.lang.value;
    this.cookieService.set('lang', this.sharedService.lang.value);
    this.ChangeCSSFile(this.cookieService.get('lang'));
  }

  switchLang(lang: string) {
    this.htmlTag.dir = lang === 'ar' ? 'rtl' : 'ltr';
    this.htmlTag.lang = lang === 'ar' ? 'ar' : 'en';
    this.lang = this.htmlTag.lang;
    this.sharedService.lang.next(this.lang);
    this.ChangeCSSFile(lang);
    this.translate.use(lang);
    sessionStorage.setItem('lang', this.sharedService.lang.value);
    this.cookieService.set('lang', this.sharedService.lang.value);
  }

  ChangeCSSFile(lang: string) {
    let HeadTag = this.document.getElementsByTagName(
      'head'
    )[0] as HTMLHeadElement;
    let existingLink = this.document.getElementById(
      'lang/CSS'
    ) as HTMLLinkElement;
    let existingCustomLink = this.document.getElementById(
      'lang/CustomCSS'
    ) as HTMLLinkElement;

    let bundlename = lang === 'ar' ? 'arabicCSS.css' : 'englishCSS.css';
    let custombundlename = lang === 'ar' ? 'customAr.css' : 'customEn.css';
    if (existingLink) {
      existingLink.href = bundlename;
      existingCustomLink.href = custombundlename;
    } else {
      let newlink = this.document.createElement('link');
      newlink.rel = 'stylesheet';
      newlink.type = 'text/css';
      newlink.href = bundlename;
      newlink.id = 'lang/CSS';
      HeadTag.appendChild(newlink);
      let newCustomlink = this.document.createElement('link');
      newCustomlink.rel = 'stylesheet';
      newCustomlink.type = 'text/css';
      newCustomlink.href = custombundlename;
      newCustomlink.id = 'lang/CustomCSS';
      HeadTag.appendChild(newCustomlink);
    }
  }
  logout() {
    this.accountService.logout();
  }

  navigateToMainMenu() {
    this.router.navigate(['/home']);
  }
}
