import { Component, forwardRef, Inject, ViewChild, ChangeDetectorRef, AfterViewInit } from '@angular/core';
import { OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { faTrash, faPenToSquare, faListDots, faArrowAltCircleLeft } from '@fortawesome/free-solid-svg-icons';
import { TranslateService } from '@ngx-translate/core';
import { FormGroup, Validators } from '@angular/forms';
import { Response } from '../../../../models/Common/response';
import { FormControl } from '@angular/forms';
import { Subject, Subscription } from 'rxjs';

import { Title } from "@angular/platform-browser";
import { DataTableDirective } from 'angular-datatables';

import * as XLSX from 'xlsx';
import { ClipboardService } from 'ngx-clipboard';
import { SharedService } from '../../../services/shared.service';
import { CommonClass } from '../../../common/CommonClass';
import { AppComponent } from '../../../app.component';
import { UserInfoServices } from '../../../../services/userinfo.service';
import { FieldObjectEmmiter, ScreenField } from '../../../../models/ScreenManagement/ScreenField';
import UserData from '../../../../models/Common/userdata';
import { CommonServices } from '../../../../services/Common/common.service';
import { LookupSetTerm } from '../../../../models/LookupSetTerm/LookupSetTerm';
import { Lookup } from '../../../../models/Common/Lookup';
import { ScreenFieldService } from '../../../../services/Screen/screen-field.service';
declare var $: any;

@Component({
  selector: 'field-permission',
  templateUrl: './field-permission.component.html',
  styleUrls: ['./field-permission.component.css']
})

export class FieldPermissionComponent implements OnInit {
  dtOptions: any = {};
  @ViewChild(DataTableDirective)
  dtElement!: DataTableDirective;
  dtTrigger: any = new Subject();
  private _searchCriteria: any = { isPageLoad: true, filter: '' };
  faTrash = faTrash;
  faPen = faPenToSquare;
  faArrowAltCircleLeft = faArrowAltCircleLeft;

  langServiceSupscribtion: Subscription | undefined;
  lang: string = 'en';
  selectedFields: ScreenField[];
  clickedField: FieldObjectEmmiter;
  currentUser: UserData = <UserData>{};
  lookupList: Lookup[];
  preview: string = '';
  selectedRoleId: string;
  constructor(
    @Inject(forwardRef(() => AppComponent)) private app: AppComponent,
    private translateService: TranslateService,
    private userService: UserInfoServices,
    private router: Router,
    private route: ActivatedRoute,
    private sharedService: SharedService,
    private commonClass: CommonClass,
    private commonServices: CommonServices,
    private screenFieldServices: ScreenFieldService,
    private titleService: Title,
    private clipboardApi: ClipboardService) {
    this.route.queryParams.subscribe(params => {
      this.selectedRoleId = params["roleId"];
      this.selectedScreenRole = params["screenId"];

    });
  }
  selectedScreenRole: string ;

  getUserData(): void {
    var _this = this;

    _this.currentUser=_this.userService.getCurrentUser();
    _this.getData();
  }

  ngOnInit(): void {
    this.langServiceSupscribtion = this.sharedService.lang.subscribe(
      (l) => (this.lang = l)
    );
  }

  OnScreenClicked(event: any) {

    this.selectedFields = event;
  }
  OnClickedField(event: any) {
    console.log(event)
    this.clickedField = event;
    this.getListData(this.clickedField);
  }
  onSave(event: Lookup[]) {
    this.save(event);
  }

  save(data: Lookup[]) {
    var lang = this.translateService.currentLang;
    //this.preview = JSON.stringify(data);
    var _this = this;
    _this.screenFieldServices.SaveFieldPermissions(data).subscribe(function (data) {
          var response = data as Response<any>;
          if (response.succeeded) {
            _this.app.messageService.add({ severity: 'success', key: 'PlanValidation', summary: _this.translateService.instant('General.SavedSuccessfully'), detail: _this.translateService.instant('General.SavedSuccessfully'), life: 6000 });
          } else {
            _this.app.messageService.add({ severity: 'error', key: 'PlanValidation', summary: _this.translateService.instant('General.Error'), detail: _this.translateService.instant('General.Error') + " " + response.errors.join('=>'), life: 6000 });
      }


        })
  }
  getListData(field: FieldObjectEmmiter) {
    var _this = this;
    _this.lookupList = [];

    if (field.isLov == false) {

      _this.screenFieldServices.GetFieldPermissionsById(field.fieldId, _this.selectedRoleId).subscribe(function (data) {

        var permissionsResponse = data as Response<Lookup[]>;
        if (permissionsResponse.succeeded) {
          _this.commonServices.getLookupsBySetId(field.lookupId).subscribe(function (data) {

            var response = data as Response<LookupSetTerm[]>;
            if (response.succeeded) {

              if (response.data.length > 0) {
                response.data.forEach(function (row) {
                  var index = permissionsResponse.data.findIndex(x => x.attributeId == +row.value && x.fieldId == field.fieldId);
                  if (index > -1) {
                    const obj = permissionsResponse.data.find(x => x.attributeId == +row.value && x.fieldId == field.fieldId);
                    _this.lookupList.push({ attributeId: +row.value, valueAr: row.displayNameAr, valueEn: row.displayNameEn, fieldId: field.fieldId, roleId: _this.selectedRoleId, fieldPermissionId: obj.fieldPermissionId });
                  }
                  else
                    _this.lookupList.push({ attributeId: +row.value, valueAr: row.displayNameAr, valueEn: row.displayNameEn, fieldId: field.fieldId, roleId: _this.selectedRoleId, fieldPermissionId: 0 });
                });

              }
            }
            else {
              _this.app.messageService.add({ severity: 'error', key: 'PlanValidation', summary: _this.translateService.instant('General.Error'), detail: _this.translateService.instant('General.Error') + " " + response.errors.join('=>'), life: 6000 });
            }

          })

        }
        else {
          _this.app.messageService.add({ severity: 'error', key: 'PlanValidation', summary: _this.translateService.instant('General.Error'), detail: _this.translateService.instant('General.Error') + " " + permissionsResponse.errors.join('=>'), life: 6000 });
        }

      })

    
    }
  }
  Back() {
    this.router.navigate(['role-screens'], { queryParams: { Id: this.selectedRoleId } });
  }

  getData() {
    var _this = this;
    $('#datatableexample2').DataTable().destroy();

    setTimeout(() => {
      $('#datatableexample2').DataTable({
        pagingType: 'full_numbers',
        pageLength: 5,
        processing: true,
        lengthMenu: [5, 10, 25]
      });
    }, 1);
  }

}
