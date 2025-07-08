use std::env;
use std::io::{Result};
use minitools::{read_lines, path_exists};
use idcard::{Gender, Identity};
use std::fs;
use std::fs::File;
use std::io::{self, Write, BufRead};
use std::path::Path;

fn main() -> Result<()> {
    // let id = Identity::new("110311195803151581");
    // println!("{:?}", id.gender());
    let output = "output.txt";

    if let Ok(lines) = read_lines("input.txt") {
        fs::remove_file(output)?;
        let mut output = File::create(output)?;
        for line in lines {
            //let l: Vec<&str> = "lion::tiger::leopard".split("::").collect();
            let l = line.unwrap();
            let x = l.split("----").collect::<Vec<_>>();
            if x.len() < 2
            {
                continue;
            } else {
                let id = Identity::new(x[1]);
                if let Some(sex) = id.gender()
                {
                    if sex == Gender::Female
                    {
                        //println!("{:?}", sex);
                        let _ = writeln!(output, "{}", l);
                    }
                }
            }
            println!("{:?}", x[1]);
        }
    }
    Ok(())
}
