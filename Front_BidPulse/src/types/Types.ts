export type AuctionLists = {
  auctionId: number;
  title: string;
  startPrice: number;
  endDate: string;
  userId: number;
  imageUrl?: string;
};

export type AuctionDetail = {
  auctionId: number;
  title: string;
  description?: string;
  startPrice: number;
  startDate: string;
  endDate: string;
  userId: number;
  userName: string;
  imageUrl?: string;
};

export type LoginValues = {
  email: string;
  password: string;
};

export type RegisterValues = {
  name: string;
  email: string;
  password: string;
};

export type User = {
  userId: number;
  name: string;
  email: string;
};

export type Bid = {
  bidId: number;
  auctionId: number;
  userId: number;
  userName: string;
  bidAmount: number;
  createdAt: string;
};
export type CreateBidDTO = {
  auctionId: number;
  userId: number;
  bidAmount: number;
};

export type CreateAuctionDTO = {
  title: string;
  description: string;
  startPrice: number;
  startDate: string;
  endDate: string;
  userId: number;
  imageUrl?: string;
};
