import { Component, OnInit } from '@angular/core';
import { faFolderOpen, faAddressCard, faHistory } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-change-facility-id',
  templateUrl: './change-facility-id.component.html',
  styleUrls: ['./change-facility-id.component.css']
})
export class ChangeFacilityIdComponent implements OnInit {
  faFolderOpen = faFolderOpen;
  faAddressCard = faAddressCard;
  faHistory = faHistory;

  constructor() { }

  ngOnInit(): void {
  }

}
