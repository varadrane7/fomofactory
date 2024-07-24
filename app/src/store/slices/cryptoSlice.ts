import { createSlice, PayloadAction } from "@reduxjs/toolkit";

interface CryptoState {
  selectedCrypto: string;
}

const initialState: CryptoState = {
  selectedCrypto: typeof window !== "undefined" && localStorage.getItem("selectedCrypto") || "BTC",
};

const cryptoSlice = createSlice({
  name: "crypto",
  initialState,
  reducers: {
    setSelectedCrypto: (state, action: PayloadAction<string>) => {
      state.selectedCrypto = action.payload;
      localStorage.setItem("selectedCrypto", action.payload); // Save to localStorage
    },
  },
});

export const { setSelectedCrypto } = cryptoSlice.actions;
export default cryptoSlice.reducer;