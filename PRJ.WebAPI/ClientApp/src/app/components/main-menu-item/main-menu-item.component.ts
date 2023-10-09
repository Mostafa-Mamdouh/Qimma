import { Component, OnInit, Input } from '@angular/core';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { SharedService } from 'src/app/services/Shared/shared.service';

@Component({
  selector: 'app-main-menu-item',
  templateUrl: './main-menu-item.component.html',
  styleUrls: ['./main-menu-item.component.css'],
})
export class MainMenuItemComponent implements OnInit {
  @Input() menu?: any;
  lang: string = 'en';
  langServiceSupscribtion: Subscription;

  constructor(private _router: Router, private sharedService: SharedService) {}

  ngOnInit(): void {
    this.langServiceSupscribtion = this.sharedService.lang.subscribe(
      (l) => (this.lang = l)
    );
  }

  action(path: string) {
    this._router.navigate([path]);
  }
}
