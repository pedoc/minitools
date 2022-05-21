use std::env;
use clap::{Parser};
use minitools::{filter_files, path_exists};
use std::io::{Result};

#[derive(Parser, Debug)]
#[clap(author, version, about, long_about = None)]
struct Args {
    #[clap(short, long)]
    path: String,

    #[clap(short, long)]
    ext: String,

    #[clap(short, long, default_value_t = 1)]
    count: u8,
}

fn main() -> Result<()> {
    let args = Args::parse();

    let exists = path_exists(&args.path);
    if !exists {
        println!("{} not exist", args.path);
    } else {
        let files = filter_files(&args.path, &args.ext);
        if files.is_empty() {
            println!("{} not found in {}", args.ext, args.path);
        } else {
            let def_file = files.first();
            println!("opening {}", def_file.unwrap().display());
            open::that(def_file.unwrap());
        }
    }
    Ok(())
}
