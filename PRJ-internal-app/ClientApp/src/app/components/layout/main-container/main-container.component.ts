import { Component, Input, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { NavigationEnd, Router } from '@angular/router';

@Component({
  selector: 'app-main-container',
  templateUrl: './main-container.component.html',
  styleUrls: ['./main-container.component.css'],
})
export class MainContainerComponent implements OnInit {
  urlArray: any;
  @Input() showBackground: boolean;

   //get isShowBackground() {
   //  return this.showBackground;
   //}

  constructor(private route: Router) {}

  ngOnInit(): void {
    this.route.events.pipe().subscribe((value) => {
      if (value instanceof NavigationEnd) {
        this.urlArray = this.route.url.toString().split('/');
        if (this.urlArray[1] == 'home') {
          this.showBackground = false;
        } else {
          this.showBackground = true;
        }
      }
    });
  }
}
