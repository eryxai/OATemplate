import { Component, OnInit } from '@angular/core';
import { DashboardService } from '../../../services/dashboard.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Activity, DashboardData, ActivityEnum } from 'src/app/dashboard-data';
import { PermissionEnum } from 'src/app/sharedFeatures/enum/permission-enum';
import { getActionCache } from '@angular/core/primitives/event-dispatch';
import { CurrentUserService } from 'src/app/sharedFeatures/services/current-user.service';
import { UserLoggedIn } from 'src/app/sharedFeatures/models/user-login.model';

@Component({
  selector: 'app-admin-dashboard',
  templateUrl: './admin-dashboard.component.html',
  styleUrls: ['./admin-dashboard.component.scss'],
})
export class AdminDashboardComponent implements OnInit {
  permissionEnum: any = PermissionEnum;
  activityEnum: any = ActivityEnum;
  filterOptions: any[] = [
    {
      name: 'Today',
      id: 'test',
    },
    {
      name: 'Yasterday',
      id: 'test',
    },
  ];



  activities: Activity[];
  dashboardData: any;
  currentUser: UserLoggedIn | null;
  constructor(
    private dashboardService: DashboardService,
    private route: ActivatedRoute,
    private currentUserService: CurrentUserService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.currentUser = this.currentUserService.getCurrentUser();

  }

}