import { useNavigate } from "react-router";
import type { AuctionLists } from "../../types/Types";
import styles from "./AuctionCard.module.css";

interface Props {
  auction: AuctionLists;
}

const AuctionCard = ({ auction }: Props) => {
  const navigate = useNavigate();

  return (
    <button
      type="button"
      className={styles.card}
      onClick={() => navigate(`/auctions/${auction.auctionId}`)}
    >
      <div className={styles.imageWrap}>
        {auction.imageUrl ? (
          <img
            src={auction.imageUrl}
            alt={auction.title}
            className={styles.image}
          />
        ) : (
          <div className={styles.imagePlaceholder} />
        )}
      </div>

      <div className={styles.content}>
        <h3 className={styles.title}>{auction.title}</h3>
        <p className={styles.text}>{auction.startPrice} kr</p>
        <p className={styles.text}>
          Ends: {new Date(auction.endDate).toLocaleDateString("sv-SE")}
        </p>
      </div>
    </button>
  );
};

export default AuctionCard;
