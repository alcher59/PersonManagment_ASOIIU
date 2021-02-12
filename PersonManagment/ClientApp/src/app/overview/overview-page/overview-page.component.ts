import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-overview-page',
  templateUrl: './overview-page.component.html',
  styleUrls: ['./overview-page.component.scss']
})
export class OverviewPageComponent implements OnInit {
  public forecasts: WeatherForecast[];
  constructor(private http: HttpClient, @Inject('BASE_URL') private  baseUrl: string) { }

  ngOnInit(): void {
    this.http.get<WeatherForecast[]>(this.baseUrl + 'api/weatherforecast').subscribe(result => {
      this.forecasts = result;
    }, error => console.error(error));
  }

}

interface WeatherForecast {
  date: string;
  temperatureC: number;
  temperatureF: number;
  summary: string;
}
