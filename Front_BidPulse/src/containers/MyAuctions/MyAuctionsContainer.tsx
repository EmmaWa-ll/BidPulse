import { useEffect, useState } from "react";
import { useNavigate } from "react-router";
import MyAuctionView from "../../components/MyAuctions/MyAuctionsView";
import type { AuctionLists, User } from "../../types/Types";
import { getMyAuctions } from "../../services/AuctionService";

const MyAuctionContainer = () => {
  const [auctions, setAuctions] = useState<AuctionLists[]>([]);
  const navigate = useNavigate();

  useEffect(() => {
    const user: User | null = JSON.parse(
      localStorage.getItem("user") || "null",
    );
    if (!user) return;

    getMyAuctions(user.userId).then(setAuctions).catch(console.error);
  }, []);

  return (
    <MyAuctionView
      auctions={auctions}
      onSelect={(id) => navigate(`/auctions/${id}`)}
    />
  );
};

export default MyAuctionContainer;
