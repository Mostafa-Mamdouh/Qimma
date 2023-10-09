import { Component, forwardRef, Inject, ViewChild, ChangeDetectorRef, AfterViewInit, Input, Output, EventEmitter, OnChanges, SimpleChanges } from '@angular/core';
import { OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { faTrash, faPenToSquare, faListDots, faArrowAltCircleLeft, faAlignRight, faAnglesRight, faAngleRight, faAnglesLeft, faAngleLeft } from '@fortawesome/free-solid-svg-icons';
import { TranslateService } from '@ngx-translate/core';
import { FormGroup, Validators } from '@angular/forms';

import { FormControl } from '@angular/forms';
import { Subject, Subscription } from 'rxjs';

import { Title } from "@angular/platform-browser";
import { DataTableDirective } from 'angular-datatables';

import * as XLSX from 'xlsx';
import { ClipboardService } from 'ngx-clipboard';
import UserData from '../../../../models/Common/userdata';
import { AppComponent } from '../../../app.component';
import { UserInfoServices } from '../../../../services/userinfo.service';
import { SharedService } from '../../../services/shared.service';
import { CommonClass } from '../../../common/CommonClass';
import { Response } from '../../../../models/Common/response';
import { faUserCircle } from '@fortawesome/free-solid-svg-icons';
import { Lookup } from '../../../../models/Common/Lookup';


declare var $: any;

@Component({
  selector: 'select-area',
  templateUrl: './select-area.component.html',
  styleUrls: ['./select-area.component.css']
})

export class SelectAreaComponent implements OnInit {
  faAnglesRight = faAnglesRight;
  faAngleRight = faAngleRight;
  faAnglesLeft = faAnglesLeft;
  faAngleLeft = faAngleLeft;

  currentUser: UserData = <UserData>{};
  lang: string = 'en';
  langServiceSupscribtion: Subscription | undefined;
  @Input() datalist: Lookup[] = [];
   sourseSelect: Lookup[] = [];
   destSelect: Lookup[] = [];
  @Output() savedFields = new EventEmitter<Lookup[]>();
  sourseSelectedItem: number[];
  destSelectedItem: number[];
  @Input() currentRole: string;

  constructor(
    @Inject(forwardRef(() => AppComponent)) private app: AppComponent,
    private translateService: TranslateService,
    private userService: UserInfoServices,
    private router: Router,
    private route: ActivatedRoute,
    private sharedService: SharedService,
    private commonClass: CommonClass,
    private titleService: Title,
    private clipboardApi: ClipboardService) {
    this.route.queryParams.subscribe(params => { });
  }


  ngOnChanges() {
    var _this = this;
    this.sourseSelectedItem = [];
    this.destSelectedItem = [];
    this.sourseSelect = [];
    this.destSelect = [];

    setTimeout(function () {

      if (_this.datalist) {
        console.log("datalist")
        console.log(_this.datalist)
        console.log(_this.datalist.length)
        console.log(_this.currentRole)


        _this.datalist.forEach(function (row) {
          if (row.fieldPermissionId > 0 && row.roleId== _this.currentRole)
            _this.destSelect.push(row);
          else
            _this.sourseSelect.push(row);
        });
      }
    }, 2000);
  
  }

  ngOnInit(): void {
    this.langServiceSupscribtion = this.sharedService.lang.subscribe(
      (l) => (this.lang = l)
    );
  }
  ngAfterViewInit(): void {
    this.translateService.stream('ApplicationTitle').subscribe(value => {
      this.translateService.stream('Screen.PageTitle').subscribe(valueTitle => {
        this.titleService.setTitle(value + valueTitle);
      });
    });
    var me = this;
  }
  getUserData(): void {
    var _this = this;
    _this.currentUser=_this.userService.getCurrentUser();
  }


  moveItemsToRight() { // move selected items from left to right select list
    var _this = this;
    this.destSelectedItem.forEach(function (item) {

      var index = _this.destSelect.findIndex(x => x.attributeId == item);
      if (index > -1) {
        var elms = _this.destSelect.splice(index, 1);
        elms.forEach(function (row) {
          _this.sourseSelect.push(row);
        });
      }
    });
    _this.destSelectedItem = [];
 }

  moveItemsToLeft() { // move back selected items from right to left select list
    var _this = this;
    this.sourseSelectedItem.forEach(function (item) {
      var index = _this.sourseSelect.findIndex(x => x.attributeId == item);
      if (index > -1) {
        var elms = _this.sourseSelect.splice(index, 1);
        elms.forEach(function (row) {
          _this.destSelect.push(row);
        });
      }
    });
    _this.sourseSelectedItem = [];
 
  }

  moveAllItemsToDest() { // move all items from left to right select list
    
    var _this = this;
    _this.sourseSelect.forEach(function (elem) {
      _this.destSelect.push(elem);
    });
    _this.sourseSelect = [];
    _this.sourseSelectedItem = [];
    _this.destSelectedItem = [];
  }

   moveAllItemsToSource() { // move back all available items from right to left select list
     var _this = this;
     _this.destSelect.forEach(function (elem) {
       _this.sourseSelect.push(elem);
     });
     _this.destSelect = [];
    _this.sourseSelectedItem = [];
    _this.destSelectedItem = [];
  }

  save() {
    if (this.destSelect.length == 0) this.destSelect.push({ attributeId: 0, valueAr: '', valueEn: '0', fieldId: this.sourseSelect.length > 0 ? this.sourseSelect[0].fieldId : 0, roleId: this.sourseSelect[0].roleId, fieldPermissionId: 0 })
    this.savedFields.emit(this.destSelect);
    this.sourseSelectedItem = [];
    this.destSelectedItem = [];
    this.sourseSelect = [];
    this.destSelect = [];
    this.datalist = [];
    window.scroll({
      top: 0,
      left: 0,
      behavior: 'smooth'
    });
  }


}
