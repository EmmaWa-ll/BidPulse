import { useEffect, useState } from "react";
import Searchbar from "../../components/SearchBar/SearchBar";
import AuctionList from "../../components/AuctionList/AuctionList";
import type { AuctionLists } from "../../types/Types";
import { getAllAuctions, searchAuctions } from "../../services/AuctionService";

const AuctionsContainer = () => {
  const [auctions, setAuctions] = useState<AuctionLists[]>([]);

  useEffect(() => {
    getAllAuctions().then((fetchedAuctions) => {
      setAuctions(fetchedAuctions);
    });
  }, []);

  function handleSearch(textTheUserTyped: string) {
    if (textTheUserTyped) {
      // Användaren har skrivit något — sök
      searchAuctions(textTheUserTyped).then((results) => {
        setAuctions(results);
      });
    } else {
      // Sökfältet är tomtvisa alla auktioner igen
      getAllAuctions().then((allAuctions) => {
        setAuctions(allAuctions);
      });
    }
  }

  return (
    <>
      <Searchbar onSearch={handleSearch} />
      <AuctionList auctions={auctions} />
    </>
  );
};

export default AuctionsContainer;
