import { DOCUMENT } from '@angular/common';
import { Component, Inject, OnInit } from '@angular/core';
import { UserDetailsService } from 'src/app/services/UserDetails/user-details.service';
import { UserDetails } from './../../../models/ProfileOfUser/UserDetails';
import { TranslateService } from '@ngx-translate/core';
import { SharedService } from 'src/app/services/Shared/shared.service';
import { DomSanitizer } from '@angular/platform-browser';
import { image } from './image.const';

@Component({
  selector: 'app-user-details',
  templateUrl: './user-details.component.html',
  styleUrls: ['./user-details.component.css'],
})
export class UserDetailsComponent implements OnInit {
  public user?: UserDetails;
  lang: string;

  constructor(
    private userDetailsService: UserDetailsService,
    private sharedService: SharedService,
    private _sanitizer: DomSanitizer
  ) {}

  ngOnInit(): void {
    this.sharedService.lang.subscribe((l) => (this.lang = l));
    this.userDetailsService.getUserDetailsById('testId').subscribe((data) => {
      this.user = <UserDetails>data;
      this.user.picture = this._sanitizer.bypassSecurityTrustResourceUrl(
        `data:image/png;base64, ${image}`
      );
    });
  }
}
