'use client'

import React, { useEffect, useState } from "react";
import axios from "axios";
import { useDispatch } from "react-redux";
import { setSelectedCrypto } from "../store/slices/cryptoSlice";
import {
  List,
  ListItem,
  ListItemButton,
  ListItemText,
  Typography,
  Dialog,
  DialogTitle,
  DialogContent,
  DialogActions,
  Button,
} from "@mui/material";

interface CryptoModalProps {
  open: boolean;
  onClose: () => void;
}

const CryptoModal: React.FC<CryptoModalProps> = ({ open, onClose }) => {
  const [cryptos, setCryptos] = useState<string[]>([]);
  const dispatch = useDispatch();

  useEffect(() => {
    const fetchCryptos = async () => {
      const response = await axios.get("http://localhost:5214/coins");
      setCryptos(response.data);
    };

    fetchCryptos();
  }, []);

  const handleSelectCrypto = (crypto: string) => {
    dispatch(setSelectedCrypto(crypto));
    onClose();
  };

  return (
    <Dialog open={open} onClose={onClose}>
      <DialogTitle>Select Cryptocurrency</DialogTitle>
      <DialogContent>
        <List>
          {cryptos.map((crypto) => (
            <ListItem key={crypto} disablePadding>
              <ListItemButton onClick={() => handleSelectCrypto(crypto)}>
                <ListItemText primary={crypto} />
              </ListItemButton>
            </ListItem>
          ))}
        </List>
      </DialogContent>
      <DialogActions>
        <Button onClick={onClose}>Cancel</Button>
      </DialogActions>
    </Dialog>
  );
};

export default CryptoModal;