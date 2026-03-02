import { useEffect, useState } from "react";
import { NavLink } from "react-router";
import AuctionList from "../AuctionList/AuctionList";
import styles from "./MyAuctionView.module.css";
import { getMyAuctions } from "../../services/AuctionService";
import type { AuctionLists, User } from "../../types/Types";

const MyAuctionView = () => {
  const [auctions, setAuctions] = useState<AuctionLists[]>([]);

  useEffect(() => {
    // Hämta inloggad användare
    const user: User | null = JSON.parse(
      localStorage.getItem("user") || "null",
    );
    if (!user) return;

    // Hämta användarens auktioner
    getMyAuctions(user.userId).then((myAuctions) => {
      setAuctions(myAuctions);
    });
  }, []);

  return (
    <div className={styles.wrapper}>
      <NavLink to="/createauction" className={styles.createBtn}>
        Create Auction
      </NavLink>

      {auctions.length === 0 ? (
        <p>You have no auctions yet.</p>
      ) : (
        <AuctionList auctions={auctions} />
      )}
    </div>
  );
};

export default MyAuctionView;
