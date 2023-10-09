import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AppSettings } from '../AppSettings';
import { Helper } from '../helper/helper';
import { AccountService } from './Account/account.service';

@Injectable()
export class UserInfoServices {
  WebApiUrl: string = AppSettings.WebApiUrl;

  constructor(
    private _helper: Helper,
    private _accountService: AccountService
  ) {}

  getCurrentLoginEmployee() {
    return this._helper.CallRequestNew('api/Common/GetCureentUser');
  }
  getCurrentUser() {
    return this._accountService.getUserDetail();
  }
}
