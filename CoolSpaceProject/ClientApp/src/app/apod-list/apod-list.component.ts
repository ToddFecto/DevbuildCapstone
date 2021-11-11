import { Component, Input, OnInit } from '@angular/core';
import { APOD } from '../apod';
import { CoolSpaceService } from '../cool-space.service';

@Component({
  selector: 'app-apod-list',
  templateUrl: './apod-list.component.html',
  styleUrls: ['./apod-list.component.css']
})
export class ApodListComponent implements OnInit {
 
  ApodList?: APOD[];
  youtubeId?: string;
  newStartDate: string ='';
  newEndDate: string ='';


  constructor(private spaceService: CoolSpaceService) { }

  ngOnInit(): void {

    
  }
 

  getApodByDateRange() {
    this.spaceService.displayApodByDateRange(
      (result: any) => {
        this.ApodList = result;
       
      },
      this.newStartDate, this.newEndDate
    );
  }

}
