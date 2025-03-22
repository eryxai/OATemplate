export class UserDepartmentListViewModel {
  userId!: number;
  list: NameValueViewModel[] = [];
}

export class NameValueViewModel {
  value!: number;
  name!: string;
}
