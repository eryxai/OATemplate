import { HttpClient } from '@angular/common/http';
import { Component, Input } from '@angular/core';
import { environment } from 'src/environments/environment';
export interface IHistory {
  label: string;
  subLabel: string;
  attachment: string;
  date: string;
  startDate: string;
  endDate: string;
  notes: string;
}

@Component({
  selector: 'msn-view-history',
  templateUrl: './view-history.component.html',
  styleUrls: ['./view-history.component.scss'],
})
export class ViewHistoryComponent {
  @Input() data: IHistory[];
  @Input() header: string;
  basUrl = environment.APIUrl;
  imageUrl = '';

  constructor(private http: HttpClient) {}

  ngOnInit(): void {}
  goToLink(url: any) {
    var _url = this.basUrl + url;
    window.open(_url, '_blank');
  }

  showAttachment() {
    this.imageUrl = ``;
  }
}
