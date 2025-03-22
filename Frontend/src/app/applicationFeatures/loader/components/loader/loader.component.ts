import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-loader',
  templateUrl: './loader.component.html',
  styleUrls: ['./loader.component.sass'],
})
export class LoaderComponent implements OnInit {
  ngOnInit(): void {}
  imagePath: string = `assets/media/lg.dual-ring-loader.gif`;
  @Input() size: string = 'md';
  @Input() hidden: boolean = false;
}
