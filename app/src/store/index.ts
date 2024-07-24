import { configureStore } from '@reduxjs/toolkit';
import { useDispatch } from 'react-redux';
import dataReducer from './slices/dataSlice';
import cryptoReducer from './slices/cryptoSlice';

export const store = configureStore({
  reducer: {
    data: dataReducer,
    crypto: cryptoReducer,
  },
});

export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;
export const useAppDispatch = () => useDispatch<AppDispatch>();