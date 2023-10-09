import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Helper, ParameterClass } from 'src/app/helper/Helper';
import { UserDetails } from 'src/app/models/ProfileOfUser/UserDetails';

@Injectable({
  providedIn: 'root',
})
export class UserDetailsService {
  constructor(private helper: Helper) {}

  getUserDetailsById(id: string) {
    var params: ParameterClass[] = [new ParameterClass('id', id)];
    return this.helper.GetRequest('api/User/GetUserDetailsById', params);
  }
}
