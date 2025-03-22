import { Component, Input, OnInit } from '@angular/core';
import { CurrentUserService } from 'src/app/sharedFeatures/services/current-user.service';
import { DashboardService } from '../../services/dashboard.service';
import { UserLoggedIn } from 'src/app/sharedFeatures/models/user-login.model';

@Component({
  selector: 'chart-card',
  templateUrl: './chart-card.component.html',
  styleUrls: ['./chart-card.component.scss'],
})
export class ChartCardComponent implements OnInit {
  data: any;
  options: any;
  makerProductivity: any;
  selectedDateRange:any;
  dateRanges: any[];
  filteredData: any;

  currentUser: UserLoggedIn | null;
  fromDate: Date;
  constructor(
    private dashboardService: DashboardService,
    private currentUserService: CurrentUserService,
  ) { }
  ngOnInit() {
    this.currentUser = this.currentUserService.getCurrentUser();
    const today = new Date();
    this.fromDate = today; // Today's date
    this.getMakerProductivity();
    this.dateRanges = [
      { label: 'Today', value: 'today' },
      { label: 'Last 7 Days', value: 'last7days' },
      { label: 'Last 30 Days', value: 'last30days' },
      { label: 'This Month', value: 'thisMonth' },
      { label: 'Last Month', value: 'lastMonth' }
    ];
    this.selectedDateRange = 'today';

    this.options = {
      scales: {
        y: {
          beginAtZero: true,
          title: {
            display: true,
            text: 'Count'
          }
        }
      },
      plugins: {
        legend: {
          display: false
        }
      },
      responsive: true,
      maintainAspectRatio: false,
    };
  }

  getMakerProductivity() {
    let search :any={};
    search.makerId= this.currentUser?.id;
    search.fromDate= this.fromDate;
    this.dashboardService.GetMakerProductivity(search).subscribe(response => {
      this.makerProductivity = response;
      this.data = {
        labels: ['Customer', 'Line'],
        datasets: [
          {
            label: 'Current Statistics',
            backgroundColor: ['#DAA520', '#007bff'], // Gold and Blue colors
            borderColor: ['#DAA520', '#007bff'],
            borderWidth: 1,
            data: [this.makerProductivity.allCustomerCount, this.makerProductivity.allCustomerLineCount] // Replace with your actual data
          }
        ]
      };
    });
  }

  filterChart() {
    const today = new Date();
  
    if (this.selectedDateRange === 'today') {
      this.fromDate = today; // Today's date
    } else if (this.selectedDateRange === 'last7days') {
      this.fromDate = new Date(today.getFullYear(), today.getMonth(), today.getDate() - 7);
    } else if (this.selectedDateRange === 'last30days') {
      this.fromDate = new Date(today.getFullYear(), today.getMonth(), today.getDate() - 30);
    } else if (this.selectedDateRange === 'thisMonth') {
      this.fromDate = new Date(today.getFullYear(), today.getMonth(), 1); // First day of the current month
    } else if (this.selectedDateRange === 'lastMonth') {
      this.fromDate = new Date(today.getFullYear(), today.getMonth() - 1, 1); // First day of the last month
      const lastDayOfLastMonth = new Date(today.getFullYear(), today.getMonth(), 0);
    }
    this.getMakerProductivity();

    // Assuming you use `fromDate` and `toDate` to filter your chart data.
  
    // Your logic to filter the chart data based on `fromDate` and `today`
    // For example, you might want to filter your data array by these dates.
  }
  
}
