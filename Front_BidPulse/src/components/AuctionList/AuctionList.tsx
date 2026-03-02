import type { AuctionLists } from "../../types/Types";
import AuctionCard from "../AuctionCard/AuctionCard";
import styles from "./AuctionList.module.css";

interface Props {
  auctions: AuctionLists[];
}

const AuctionList = ({ auctions }: Props) => {
  return (
    <div className={styles.list}>
      {auctions.map((auction) => (
        <AuctionCard key={auction.auctionId} auction={auction} />
      ))}
    </div>
  );
};

export default AuctionList;
