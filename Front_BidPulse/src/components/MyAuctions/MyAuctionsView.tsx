import { NavLink } from "react-router";
import AuctionList from "../AuctionList/AuctionList";
import styles from "./MyAuctionView.module.css";
import type { AuctionLists } from "../../types/Types";

interface Props {
  auctions: AuctionLists[];
  onSelect: (id: string) => void;
}

const MyAuctionView = ({ auctions, onSelect }: Props) => {
  return (
    <div className={styles.wrapper}>
      <NavLink to="/createauction" className={styles.createBtn}>
        Create Auction
      </NavLink>

      {auctions.length === 0 ? (
        <p>You have no auctions yet.</p>
      ) : (
        <AuctionList auctions={auctions} onSelect={onSelect} />
      )}
    </div>
  );
};

export default MyAuctionView;
