import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { faStopCircle, faArrowLeft, faArrowRight, faGear } from '@fortawesome/free-solid-svg-icons';
import { SharedService } from '../../../../services/shared.service';

@Component({
  selector: 'app-sidebar-category-item',
  templateUrl: './sidebar-category-item.component.html',
  styleUrls: ['./sidebar-category-item.component.css'],
})
export class SidebarCategoryItemComponent implements OnInit {
  faStopCircle = faStopCircle;
  faArrow = faArrowLeft;
  faGear = faGear;
  clicked: boolean = false;

  @Input() menu: any = [];
  // @Input() icon: any = faStopCircle;

  constructor(private shared: SharedService, private router: Router) { }

  ngOnInit(): void {
    this.shared.lang.subscribe((value) => {
      value == 'en' ? (this.faArrow = faArrowRight) : (this.faArrow = faArrowLeft);
    })
  }
  GoTO(path: string) {
    this.router.navigate([path]);
  }
  toggleMenu() {
    this.clicked = !this.clicked;
    // if (this.menu.children.length > 0) {
    //   this.clicked = !this.clicked;
    // } else {
    //   sessionStorage.setItem('token', this._sharedService.token);
    //   this._router.navigate([this.menu.url], { queryParams: { lan: this.lang } });
    // }
  }
}
