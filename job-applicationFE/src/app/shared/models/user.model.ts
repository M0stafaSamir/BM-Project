export interface User {
  id: string;
  username: string;
  email: string;
  createdAt: Date;
  updatedAt: Date;
  role: 'admin' | 'user' | 'guest';
  isActive: boolean;
  profilePictureUrl?: string;
  bio?: string;
  tags?: string[];
}

export interface Login {
  email: string;
  password: string;
}
export interface Register {
  fname: string;
  lname: string;
  email: string;
  password: string;
  confirmPassword: string;
  address: string;
  phoneNo: string;
}
