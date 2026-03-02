import type { Bid } from "../../../types/Types";
import styles from "./BidsPanel.module.css";

type Props = {
  bids: Bid[];
};

const BidsPanel = ({ bids }: Props) => {
  return (
    <div className={styles.panel}>
      <div className={styles.header}>
        <span>Bids</span>
        <span>Name</span>
        <span>Date</span>
      </div>

      {bids.map((bid) => (
        <div key={bid.bidId} className={styles.row}>
          <span>{bid.bidAmount} kr</span>
          <span>{bid.userName}</span>
          <span>{new Date(bid.createdAt).toLocaleDateString("sv-SE")}</span>
        </div>
      ))}
    </div>
  );
};

export default BidsPanel;
