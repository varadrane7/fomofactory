'use client'

import React from "react";
import { useSelector } from "react-redux";
import { RootState } from "../store";
import { Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Paper } from "@mui/material";

const DataTable: React.FC = () => {
  const data = useSelector((state: RootState) => state.data.entries);

  return (
    <TableContainer component={Paper}>
      <Table>
        <TableHead>
          <TableRow>
            <TableCell>Crypto</TableCell>
            <TableCell>Price</TableCell>
            <TableCell>Timestamp</TableCell>
          </TableRow>
        </TableHead>
        <TableBody>
          {data.map((entry) => (
            <TableRow key={entry.id}>
              <TableCell>{entry.code}</TableCell>
              <TableCell>{entry.rate}</TableCell>
              <TableCell>{new Date(entry.time).toLocaleString()}</TableCell>
            </TableRow>
          ))}
        </TableBody>
      </Table>
    </TableContainer>
  );
};

export default DataTable;