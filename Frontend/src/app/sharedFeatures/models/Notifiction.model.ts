import { NotifyEntityTypeEnum } from '../enum/NotifyEntityTypeEnum';

export class NotificationReturn {
  NotifyEntityTypeEnum!: NotifyEntityTypeEnum;
  WrongTitle!: string;
  WrongMessage!: string;
  IdentityCurrentUser!: string;
  dataEn!: string;
  dataAr!: string;
  userId!: number;
}
