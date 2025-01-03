import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-paging-header',
  templateUrl: './paging-header.component.html',
  styleUrls: ['./paging-header.component.scss']
})
export class PagingHeaderComponent {
  @Input() pageNumber: number = 0;
  @Input() pageSize: number = 0;
  @Input() totalItemCount: number = 0;
}
