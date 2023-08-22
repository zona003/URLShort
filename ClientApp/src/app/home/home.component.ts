import { Component, OnInit } from '@angular/core';
import { DataService } from '../_service/data.service';
import { Url } from '../_model/Url';
import { first } from 'rxjs';


@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  providers: [DataService]
})
export class HomeComponent implements OnInit{

  urlsCollection: Url[]  = [];

  constructor(private dataService: DataService){}

  ngOnInit(): void {
    this.loadUrls()
  }

  loadUrls(){
    this.dataService.getUrls()
    .subscribe((data: Url[]) => this.urlsCollection = data)
  }

}
