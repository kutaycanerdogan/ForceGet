export class User {
  id: number;
  email: string;
  createdAt: Date;

  constructor(
    id: number = 0,
    email: string = '',
    createdAt: Date = new Date()
  ) {
    this.id = id;
    this.email = email;
    this.createdAt = createdAt;
  }
}
