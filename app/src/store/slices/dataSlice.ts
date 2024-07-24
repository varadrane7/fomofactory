import { createSlice, PayloadAction } from '@reduxjs/toolkit';

interface DataState {
  entries: any[];
}

const initialState: DataState = {
  entries: [],
};

const dataSlice = createSlice({
  name: 'data',
  initialState,
  reducers: {
    setData: (state, action: PayloadAction<any[]>) => {
      state.entries = action.payload;
    },
  },
});

export const { setData } = dataSlice.actions;
export default dataSlice.reducer;