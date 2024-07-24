'use client'
import { useEffect, useState } from "react";
import axios from "axios";
import { useSelector } from "react-redux";
import { RootState, useAppDispatch } from "../store";
import { setData } from "../store/slices/dataSlice";
import DataTable from "../components/Datatable";
import CryptoModal from "../components/CryptoModal";
import { Button } from "@mui/material";

const Home: React.FC = () => {
  const dispatch = useAppDispatch();
  const selectedCrypto = useSelector(
    (state: RootState) => state.crypto.selectedCrypto
  );
  const [open, setOpen] = useState(false);

  useEffect(() => {
    const fetchData = async () => {
      const response = await axios.get(
        `http://localhost:5214/coins/${selectedCrypto}`
      );
      dispatch(setData(response.data));
    };

    fetchData();

    const intervalId = setInterval(fetchData, 5000);
    return () => clearInterval(intervalId);
  }, [selectedCrypto, dispatch]);

  const handleOpen = () => setOpen(true);
  const handleClose = () => setOpen(false);

  return (
    <div>
      <h1>Real-Time Crypto Data</h1>
      <Button variant="contained" onClick={handleOpen}>
        Change Crypto
      </Button>
      <DataTable />
      <CryptoModal open={open} onClose={handleClose} />
    </div>
  );
};

export default Home;