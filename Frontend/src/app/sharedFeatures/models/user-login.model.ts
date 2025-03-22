export interface UserLoggedIn {
  access_token: string;
  email: string;
  expires: any;
  expires_in: any;
  nameAr: string;
  nameEn: string;
  name: string;
  id: any;
  issued: any;
  mobile: string;
  phone: string;
  permissions: number[];
  token_type: string;
  userName: string;
  isFirstTimeLogin: boolean;
  organizationId: number;
  organizationName: string;
  organizationNameEn:string;
  organizationNameAr:string;
  organizationImage: string;
  profileImage: string;
  roleName: string;
  isSuperAdmin :boolean;

}
