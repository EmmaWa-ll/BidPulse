import type {
  AuctionLists,
  AuctionDetail,
  CreateAuctionDTO,
} from "../types/Types";

const BASE_URL = "https://localhost:7147/api/auction";

//vis aöppna auktioner
export const getAllAuctions = async (): Promise<AuctionLists[]> => {
  const res = await fetch(BASE_URL);
  if (!res.ok) throw new Error("Failed to fetch auctions");
  return res.json();
};

//encodeURIComponent= specialtecken i sökord ej förstör url.(t.ex. mellasnlag och så)
//retunerarr auctionlist
export const searchAuctions = async (
  search: string,
): Promise<AuctionLists[]> => {
  const res = await fetch(`${BASE_URL}?search=${encodeURIComponent(search)}`);
  if (!res.ok) throw new Error("Failed to search auctions");
  return res.json();
};

//Skickat den get och retunerar auctiondetail som har mer info som finns i auctiondetail komponenten
export const getAuctionById = async (id: number): Promise<AuctionDetail> => {
  const res = await fetch(`${BASE_URL}/${id}`);
  if (!res.ok) throw new Error("Failed to fetch auction");
  return res.json();
};

//skickar post, med auktionsdata i bodyn. JsonStringify = omvandlar objektet till textsträng. Retunerar
export const createAuction = async (
  auction: CreateAuctionDTO,
): Promise<{ message: string }> => {
  const res = await fetch(BASE_URL, {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(auction),
  });
  if (!res.ok) throw new Error("Failed to create auction");
  return res.json();
};

//hämat ALLA auctioner i forntend med filler )
export const getMyAuctions = async (
  userId: number,
): Promise<AuctionLists[]> => {
  const auctions = await getAllAuctions();
  return auctions.filter((a) => a.userId === userId);
};
