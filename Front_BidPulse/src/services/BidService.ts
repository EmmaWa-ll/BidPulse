import type { Bid, CreateBidDTO } from "../types/Types";

const BASE_URL = `${import.meta.env.VITE_API_URL}/api/bid`;

//Skickar get och retunerar lista bed Bid för en specifik auciton.
export const getBidsForAuction = async (auctionId: number): Promise<Bid[]> => {
  const response = await fetch(`${BASE_URL}/auction/${auctionId}`);
  if (!response.ok) throw new Error("Failed to fetch bids");
  return response.json();
};

//Skickar post till api/bid  med createbiddto i bodyn.
//visar text direkt från backend, att det måste vara högre än highest bid.
export const createBid = async (
  dto: CreateBidDTO,
): Promise<{ message: string }> => {
  const response = await fetch(BASE_URL, {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(dto),
  });

  if (!response.ok) {
    const msg = await response.text();
    throw new Error(msg || "Failed to place bid");
  }

  return response.json();
};
