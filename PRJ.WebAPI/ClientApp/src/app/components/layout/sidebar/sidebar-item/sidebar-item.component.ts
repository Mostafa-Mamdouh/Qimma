import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-sidebar-item',
  templateUrl: './sidebar-item.component.html',
  styleUrls: ['./sidebar-item.component.css'],
})
export class SidebarItemComponent implements OnInit {
  // @Input() option: any;

  constructor(private router: Router) {}

  ngOnInit(): void {}

  action(path: string) {
    // this.router.navigate([path], { queryParams: { lan: this.lang } });
  }
}
