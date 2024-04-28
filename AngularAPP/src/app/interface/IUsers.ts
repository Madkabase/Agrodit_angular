export interface IUsers {
  Id: number;
FirstName: string;
LastName: string;
Email: string;
Password: string;
IsVerified: boolean;
ConfirmationCode: number;
ConfirmationExpirationDate: Date;
ConfirmationTriesCounter: number;

  actions?: string;
}
