
export interface GeneralInformation {
  allCustomers: number;
  newCustomers: number;
  allLines: number;
  newLines: number;
}

export interface Activity {
  name: string;
  userImage: string | null;
  activityType: ActivityEnum;
  creationDate: string;
  color?: string;
  alias?: string;
}

export interface DashboardData {
  generalInformation: GeneralInformation;
}
export enum ActivityEnum {
  AddCustomer = 1,
  UpdateCustomer = 2,
  DeleteCustomer = 3,
  AddCustomerLine = 4,
  UpdateCustomerLine = 5,
  DeleteCustomerLine = 6,
  AddCustomerDocument = 7,
  UpdateCustomerDocument = 8,
  DeleteCustomerDocument = 9,
  ApproveCustomer = 10,
  ApproveCustomerLine = 11,
  ApproveCustomerDocument = 12,
  RejectCustomer = 13,
  RejectCustomerLine = 14,
  RejectCustomerDocument = 15,
}
