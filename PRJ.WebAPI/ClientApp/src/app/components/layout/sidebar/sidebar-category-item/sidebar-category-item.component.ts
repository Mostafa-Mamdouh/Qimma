import { Component, Input, OnInit } from '@angular/core';
import {
  faStopCircle,
  faArrowLeft,
  faArrowRight,
} from '@fortawesome/free-solid-svg-icons';
import { SharedService } from 'src/app/services/Shared/shared.service';

@Component({
  selector: 'app-sidebar-category-item',
  templateUrl: './sidebar-category-item.component.html',
  styleUrls: ['./sidebar-category-item.component.css'],
})
export class SidebarCategoryItemComponent implements OnInit {
  faStopCircle = faStopCircle;
  faArrow = faArrowLeft;
  clicked: boolean = false;

  @Input() menu: any = [];
  // @Input() icon: any = faStopCircle;

  constructor(private shared: SharedService) {}

  ngOnInit(): void {
    this.shared.lang.subscribe((l) => {
      l == 'en' ? (this.faArrow = faArrowRight) : (this.faArrow = faArrowLeft);
    });
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
